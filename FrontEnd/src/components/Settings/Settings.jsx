import React, { useState } from 'react';

import PropTypes from 'prop-types';
import Collapsible from 'react-collapsible';

import TextInputRow from '../../ui-components/TextInputRow';

const Settings = ({
  apigeeBaseAddress,
  apigeeClientId,
  apigeeClientSecret,
  updateSettings,
}) => {
  const [baseAddress, setBaseAddress] = useState(apigeeBaseAddress);
  const [clientId, setClientId] = useState(apigeeClientId);
  const [clientSecret, setClientSecret] = useState(apigeeClientSecret);
  return (
    <Collapsible key="settings" trigger="API Settings">
      <form>
        <TextInputRow
          id="apiUrl"
          labelText="API Base URL"
          value={baseAddress}
          onChange={v => {
            setBaseAddress(v);
            updateSettings({
              apigeeBaseAddress: v,
              apigeeClientId,
              apigeeClientSecret,
            });
          }}
        />
        <TextInputRow
          id="clientId"
          labelText="Client Id"
          value={clientId}
          onChange={v => {
            setClientId(v);
            updateSettings({
              apigeeBaseAddress,
              apigeeClientId: v,
              apigeeClientSecret,
            });
          }}
        />
        <TextInputRow
          id="clientSecret"
          labelText="Client Secret"
          value={clientSecret}
          onChange={v => {
            setClientSecret(v);
            updateSettings({
              apigeeBaseAddress,
              apigeeClientId,
              apigeeClientSecret: v,
            });
          }}
        />
      </form>
    </Collapsible>
  );
};

Settings.propTypes = {
  apigeeBaseAddress: PropTypes.string.isRequired,
  apigeeClientId: PropTypes.string.isRequired,
  apigeeClientSecret: PropTypes.string.isRequired,
  updateSettings: PropTypes.func.isRequired,
};

export default Settings;
