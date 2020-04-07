import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

import { Modal } from 'react-bootstrap';

import Constants from '../../Constants';
import UploadFiles from '../../ui-components/UploadFiles';
import Settings from '../Settings';

const Home = props => {
  const [isFileUploaded, setIsFileUploaded] = React.useState(false);
  const [isOpen, setIsOpen] = React.useState(false);
  const [paymentBatchId, setPaymentBatchId] = React.useState(0);

  const { apigeeBaseAddress, apigeeClientId, apigeeClientSecret } = props;
  const fileUploadUrl = `${Constants.APP_URL}/api/FileUpload`;

  const showModal = () => {
    setIsOpen(true);
  };

  const hideModal = () => {
    setIsOpen(false);
  };

  const handleOnUploadSuccess = data => {
    console.log('data on success', data);
    setPaymentBatchId(data.paymentBatchId);
    setIsFileUploaded(true);
    showModal();
  };

  return (
    <div>
      <h1>Batch Payments Utility</h1>
      <p>
        Purpose of this utility is to create a payments batch and add
        instructions using a public url from apigee
      </p>

      <div className="container-fluid">
        <div className="row">
          <div className="col-md-10 offset-md-1">
            <Settings />
          </div>
        </div>

        <div className="mb-3" />

        <div className="row">
          <div className="col-md-10 offset-md-1">
            <UploadFiles
              settings={{
                apigeeBaseAddress,
                apigeeClientId,
                apigeeClientSecret,
              }}
              validFileExtensions=".csv"
              fileUploadUrl={fileUploadUrl}
              maxNumberOfFiles={1}
              onUploadError={() => {
                showModal();
              }}
              onUploadSuccess={data => handleOnUploadSuccess(data)}
            />
          </div>
        </div>
        <Modal show={isOpen} onHide={hideModal}>
          <Modal.Header>
            <Modal.Title>Created Payment Batch</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            {isFileUploaded ? (
              paymentBatchId
            ) : (
              <p>An Error occurred while uploading file</p>
            )}
          </Modal.Body>
        </Modal>
      </div>
    </div>
  );
};

Home.propTypes = {
  apigeeBaseAddress: PropTypes.string.isRequired,
  apigeeClientId: PropTypes.string.isRequired,
  apigeeClientSecret: PropTypes.string.isRequired,
};

const mapStateToProps = ({ settings }) => {
  return {
    ...settings,
  };
};

export default connect(mapStateToProps)(Home);
