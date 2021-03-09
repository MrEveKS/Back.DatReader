import React from 'react';
import {BrowserRouter as Router, Route, Switch,Redirect} from "react-router-dom";
import {createBrowserHistory} from 'history';

import AppBody from "./AppBody.jsx";

const Ip = (props) => {
	return <AppBody { ...props } search="ip" />
}

const Locations = (props) => {
	return <AppBody { ...props } search="city" />
}

const App = () => {
	const history = createBrowserHistory();
	return (<Router>
		<Redirect from="/" to="/ip" />
		<Switch>
			<Route history={history} exact path="/ip" component={Ip}/>
			<Route history={history} exact path="/city" component={Locations}/>
		</Switch>
	</Router>)
}

export default App;
