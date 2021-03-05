import React from 'react';
import {BrowserRouter as Router, Route, Switch,Redirect} from "react-router-dom";

import AppBody from "./AppBody.jsx";

const Ip = () => {
	return <AppBody {...{search: 'ip'}} />
}

const Locations = () => {
	return <AppBody {...{search: 'locations'}} />
}

const App = () => {
	return (<Router>
		<Redirect from="/" to="/ip/location" />
		<Switch>
			<Route exact path="/" component={Ip}/>
			<Route exact path="/ip/location" component={Ip}/>
			<Route exact path="/city/locations" component={Locations}/>
		</Switch>
	</Router>)
}

export default App;
