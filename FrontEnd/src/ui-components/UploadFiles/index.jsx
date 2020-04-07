import React from 'react';
import PropTypes from 'prop-types';

// Uppy imports
import Uppy from '@uppy/core';
import XHRUpload from '@uppy/xhr-upload';
import { Dashboard } from '@uppy/react';

class UploadFiles extends React.Component {
  constructor(props) {
    super(props);

    const uppyFileRestrictions = {
      maxFileSize: 1024 * 1024 * 5,
      maxNumberOfFiles: props.maxNumberOfFiles,
    };

    const { validFileExtensions } = props;
    const validFileExtensionsArray = validFileExtensions.split(',');
    if (validFileExtensionsArray.length)
      uppyFileRestrictions.allowedFileTypes = validFileExtensionsArray;

    this.fileUploaderUppy = new Uppy({
      id: 'file',
      restrictions: uppyFileRestrictions,
    });
  }

  componentDidMount() {
    const {
      fileUploadUrl,
      onUploadProgress,
      onUploadSuccess,
      onUploadError,
    } = this.props;
    try {
      this.fileUploaderUppy.use(XHRUpload, {
        endpoint: fileUploadUrl,
        formData: true,
        fieldName: 'fileAttachment',
      });

      this.fileUploaderUppy.on('upload-progress', (file, progress) => {
        // file: { id, name, type, ... }
        // progress: { uploader, bytesUploaded, bytesTotal }
        onUploadProgress(file, progress);
      });

      this.fileUploaderUppy.on('upload-success', (file, response) => {
        // do something with extracted response data
        // (`body` is equivalent to `file.response.body` or `uppy.getFile(fileID).response.body`)
        const { isSuccess, result, error } = response.body;
        if (!isSuccess) {
          onUploadError({
            message: error.message || 'Server Error - Failed to upload file',
          });
          return;
        }

        onUploadSuccess({
          ...result,
        });
      });
      this.fileUploaderUppy.on('upload-error', (file, errorResponse) => {
        if (errorResponse.request) {
          const { responseText, statusText } = errorResponse.request;
          onUploadError({
            message: responseText.length < 256 ? responseText : statusText,
          });
          return;
        }
        onUploadError({
          message: errorResponse || 'Failed to upload file',
        });
      });
    } catch (errorResponse) {
      const { xhr, error } = errorResponse;
      onUploadError({
        message:
          xhr && xhr.responseText && xhr.responseText.length < 256
            ? xhr.responseText
            : error,
      });
    }
  }

  render() {
    const localeStrings = {
      dropPaste: 'Drag & drop or %{browse} file(s) to upload',
    };

    const { settings } = this.props;
    this.fileUploaderUppy.setMeta({ ...settings });

    return (
      <div className="row">
        <div className="col-sm-12">
          <Dashboard
            id="fileUploadDashboard"
            closeModalOnClickOutside
            height={470}
            proudlyDisplayPoweredByUppy={false}
            note="Maximum file upload size is 5 MB"
            uppy={this.fileUploaderUppy}
            locale={{
              strings: localeStrings,
            }}
          />
        </div>
      </div>
    );
  }
}

UploadFiles.propTypes = {
  settings: PropTypes.shape({
    apigeeBaseAddress: PropTypes.string.isRequired,
    apigeeClientId: PropTypes.string.isRequired,
    apigeeClientSecret: PropTypes.string.isRequired,
  }).isRequired,
  maxNumberOfFiles: PropTypes.number,
  validFileExtensions: PropTypes.string,
  fileUploadUrl: PropTypes.string.isRequired,
  onUploadProgress: PropTypes.func,
  onUploadSuccess: PropTypes.func,
  onUploadError: PropTypes.func,
};

UploadFiles.defaultProps = {
  validFileExtensions: '',
  maxNumberOfFiles: 5,
  onUploadProgress: () => {},
  onUploadSuccess: () => {},
  onUploadError: () => {},
};

export default UploadFiles;
