import React, {useEffect, useState} from 'react';
import Paper from '@material-ui/core/Paper';
import {withStyles} from '@material-ui/core/styles';

import VirtualizedTable from './VirtualizedTable.jsx';
import SearchBar from './SearchBar.jsx';

import QueryService from '../services/QueryService';

const {setTimeout, clearTimeout} = window;

const styles = (theme) => ({
	root: {
		paddingTop: theme.mixins.toolbar.minHeight,
		height: '100%',
		width: '100%',
	},
	tableContainer: {
		marginTop: 1,
		height: 'calc(100% - 42px)'
	}
});

let indexInQueue = -1;
let gottenIndex = [];
let waitIndex = -1;

let waitTimer;

function MuiReactVirtualizedTable(props) {

	const emptyRow = ['', '', '', '', '', ''];
	const rowInPage = 200;

	const {classes} = props;

	const [search, setSearch] = useState('');
	const [searchData, setSearchData] = useState(false);
	const [tableData, setTableData] = useState({rowsCollection: {}, rowsCount: 0, firstColumnWidth: 54});

	useEffect(() => {
		getData(0);
	}, []);

	useEffect(() => {
		if (searchData) {
			setSearchData(false);
			getData(0, true);
		}
	}, [searchData]);

	const getData = (index, getNewCollection) => {
		if (waitIndex !== -1) {
			if (waitIndex !== index &&
				indexInQueue !== index) {
				indexInQueue = index;
			}
			return;
		}

		waitIndex = index;

		const url = 'api/UserLocation/GetAll';
		const queryService = QueryService();
		const queryData = {
			filter: {
				cityEqual: search?.trim()
			},
			withCount: true,
			take: rowInPage,
			skip: rowInPage * index
		};

		queryService.post(url, queryData)

			.subscribe((data) => {
				const newData = {...tableData, rowsCollection: {...tableData.rowsCollection}};
				const items = data?.items ?? [];
				if (items.length) {
					const lastItem = items[items.length - 1];
					if (lastItem && lastItem.id) {
						if (lastItem.id > 9 && lastItem.id % 10 === 0 && index % rowInPage !== 0) {
							const idColumnWidth = String(lastItem.id).length * 18;
							if (newData.firstColumnWidth < idColumnWidth) {
								newData.firstColumnWidth = idColumnWidth;
							}
						}
					}
				}

				newData.rowsCount = data?.count ?? 0;
				if (getNewCollection) {
					newData.rowsCollection = {[index]: items};
					gottenIndex = [];
				} else {
					newData.rowsCollection = {...newData.rowsCollection, [index]: items};
				}

				setTableData(newData);

				gottenIndex.push(index);

				if (indexInQueue !== -1 && indexInQueue !== index) {
					gottenIndex.indexOf(indexInQueue) === -1 && getNext(indexInQueue);
				}
				waitIndex = -1;
				indexInQueue = -1;
			});
	};

	const getNext = (index) => {
		waitTimer && clearTimeout(waitTimer)
		waitTimer = setTimeout(() => {
			getData(index);
		}, 50);
	};

	const onSearch = () => {
		getData(0, true);
	};

	const onClear = () => {
		setSearch('');
		setSearchData(true);
	};

	const getRow = (index) => {
		const newIndex = index % rowInPage;
		const rowIndex = (index - newIndex) / rowInPage;
		if (tableData.rowsCollection[rowIndex]) {
			return tableData.rowsCollection[rowIndex][newIndex];
		} else {
			getNext(rowIndex);
			return [index, ...emptyRow];
		}
	};

	return (
		<Paper className={classes.root}>
			<SearchBar
				placeholder="Поиск списка местоположений"
				ariaLabel="поиск списка местоположений"
				value={search}
				onChange={(searchVal) => setSearch(searchVal)}
				onSearch={() => onSearch(search)}
				onClear={onClear}
			/>
			<Paper className={classes.tableContainer}>
				<VirtualizedTable
					rowCount={tableData.rowsCount}
					rowGetter={({index}) => getRow(index)}
					columns={[
						{
							width: tableData.firstColumnWidth,
							label: '#',
							dataKey: 'id',
							numeric: true,
						},
						{
							width: 200,
							label: 'Страна',
							dataKey: 'country',
						},
						{
							width: 200,
							label: 'Область',
							dataKey: 'region',
						},
						{
							width: 200,
							label: 'Почтовый индекс',
							dataKey: 'postal',
						},
						{
							width: 200,
							label: 'Город',
							dataKey: 'city',
						},
						{
							width: 200,
							label: 'Организация',
							dataKey: 'organization',
						},
						{
							width: 200,
							label: 'Широта',
							dataKey: 'latitude',
							numeric: true,
						},
						{
							width: 200,
							label: 'Долгота',
							dataKey: 'longitude',
							numeric: true,
						},
					]}
				/>
			</Paper>
		</Paper>
	);
}

const ReactVirtualizedTable = withStyles(styles)(MuiReactVirtualizedTable);
export default ReactVirtualizedTable;
