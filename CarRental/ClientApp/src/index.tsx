import 'bootstrap/dist/css/bootstrap.css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
//import configureStore from './store/configureStore';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import dataStore from './store/DataStore';


ReactDOM.render(
    <Provider store={dataStore}>
        <App />
    </Provider>,
    document.getElementById('root'));

registerServiceWorker();
