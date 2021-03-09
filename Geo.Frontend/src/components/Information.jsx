import React, {useEffect, useState} from 'react';

import {Grid} from "@material-ui/core";
import {withStyles} from "@material-ui/core/styles";

import QueryService from "../services/QueryService";
import SearchBar from "./SearchBar.jsx";

const styles = (theme) => ({
	root: {
		paddingTop: theme.mixins.toolbar.minHeight,
		width: '100%',
	},
});

function MuiInformation(props) {
	const {classes} = props;
	const [search, setSearch] = useState('');
	const [searchData, setSearchData] = useState(false);
	const [data, setData] = useState(false);

	useEffect(() => {
		search && getData();
	}, []);

	useEffect(() => {
		if (searchData) {
			setSearchData(false);
			getData();
		}
	}, [searchData]);

	const getData = () => {
		const url = 'api/UserIp/GetUserLocation';
		const queryService = QueryService();
		const queryData = {
			filter: {
				ipAddress: search?.trim()
			},
		};

		queryService.post(url, queryData)
			.subscribe((dto) => {
				setData(dto);
			});
	}

	const onSearch = () => {
		getData(0, true);
	};

	const onClear = () => {
		setSearch('');
		setSearchData(true);
	};

	return (
		<Grid container className={classes.root}>
			<SearchBar
				placeholder="Поиска гео-информации по Ip"
				ariaLabel="поиска гео-информации по ip"
				value={search}
				onChange={(searchVal) => setSearch(searchVal)}
				onSearch={() => onSearch(search)}
				onClear={onClear}
			/>
			<Grid item xs={12} sm={6}>
				Страна: {data.country}
			</Grid>
			<Grid item xs={12} sm={6}>
				Область: {data.region}
			</Grid>
			<Grid item xs={12} sm={6}>
				Почтовый индекс: {data.postal}
			</Grid>
			<Grid item xs={12} sm={6}>
				Город: {data.city}
			</Grid>
			<Grid item xs={12} sm={6}>
				Организация: {data.organization}
			</Grid>
			<Grid item xs={12} sm={6}>
				Широта: {data.latitude}
			</Grid>
			<Grid item xs={12} sm={6}>
				Долгота: {data.longitude}
			</Grid>
		</Grid>
	);
}

const Information = withStyles(styles)(MuiInformation);
export default Information;
