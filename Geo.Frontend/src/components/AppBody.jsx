import React from 'react';

import {Link} from "react-router-dom";

import PropTypes from 'prop-types';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Divider from '@material-ui/core/Divider';
import Drawer from '@material-ui/core/Drawer';
import Hidden from '@material-ui/core/Hidden';
import IconButton from '@material-ui/core/IconButton';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Typography from '@material-ui/core/Typography';
import {makeStyles, useTheme} from '@material-ui/core/styles';

import MenuIcon from '@material-ui/icons/Menu';
import Toolbar from '@material-ui/core/Toolbar';
import DnsIcon from '@material-ui/icons/Dns';
import LocationSearchingIcon from '@material-ui/icons/LocationSearching';

import ReactVirtualizedTable from './ReactVirtualizedTable.jsx';

import "../styles/main.css";

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
	root: {
		display: 'flex',
		height: '100%',
	},
	drawer: {
		[theme.breakpoints.up('sm')]: {
			width: drawerWidth,
			flexShrink: 0,
		},
	},
	appBar: {
		[theme.breakpoints.up('sm')]: {
			width: `calc(100% - ${drawerWidth}px)`,
			marginLeft: drawerWidth,
		},
	},
	menuButton: {
		marginRight: theme.spacing(2),
		[theme.breakpoints.up('sm')]: {
			display: 'none',
		},
	},
	toolbar: theme.mixins.toolbar,
	drawerPaper: {
		width: drawerWidth,
	},
	content: {
		flexGrow: 1,
		padding: theme.spacing(1),
	},
	active: {
		backgroundColor: '#f1f1f1'
	}
}));

function AppBody(props) {
	const {window} = props;
	const classes = useStyles();
	const theme = useTheme();
	const [mobileOpen, setMobileOpen] = React.useState(false);
	const searchIp = props.search === 'ip';
	const bodyProps = {
		placeholder: searchIp ? 'Поиска гео-информации' : 'Поиск списка местоположений',
		ariaLabel: searchIp ? 'поиска гео-информации' : 'поиск списка местоположений'
	}

	const handleDrawerToggle = () => {
		setMobileOpen(!mobileOpen);
	};

	const drawer = (
		<div>
			<div className={classes.toolbar}/>
			<Divider/>
			<List>
				<ListItem button component={Link} to='ip' className={searchIp ? classes.active : ''}>
					<ListItemIcon> <DnsIcon/> </ListItemIcon>
					<ListItemText primary="Гео-информация"/>
				</ListItem>
				<ListItem button component={Link} to='city'
						  className={!searchIp ? classes.active : ''}>
					<ListItemIcon> <LocationSearchingIcon/> </ListItemIcon>
					<ListItemText primary="Местоположения"/>
				</ListItem>
			</List>
		</div>
	);

	const container = window !== undefined ? () => window().document.body : undefined;

	return (
		<div className={classes.root}>
			<CssBaseline/>
			<AppBar position="fixed" className={classes.appBar}>
				<Toolbar>
					<IconButton
						color="inherit"
						aria-label="open drawer"
						edge="start"
						onClick={handleDrawerToggle}
						className={classes.menuButton}
					>
						<MenuIcon/>
					</IconButton>
					<Typography variant="h6" noWrap>
						Responsive drawer
					</Typography>
				</Toolbar>
			</AppBar>
			<nav className={classes.drawer} aria-label="mailbox folders">
				<Hidden smUp implementation="css">
					<Drawer
						container={container}
						variant="temporary"
						anchor={theme.direction === 'rtl' ? 'right' : 'left'}
						open={mobileOpen}
						onClose={handleDrawerToggle}
						classes={{
							paper: classes.drawerPaper,
						}}
						ModalProps={{
							keepMounted: true,
						}}
					>
						{drawer}
					</Drawer>
				</Hidden>
				<Hidden xsDown implementation="css">
					<Drawer
						classes={{
							paper: classes.drawerPaper,
						}}
						variant="permanent"
						open
					>
						{drawer}
					</Drawer>
				</Hidden>
			</nav>
			<main className={classes.content}>

				<ReactVirtualizedTable {...bodyProps} />

			</main>
		</div>
	);
}

AppBody.propTypes = {
	window: PropTypes.func,
};

export default AppBody;