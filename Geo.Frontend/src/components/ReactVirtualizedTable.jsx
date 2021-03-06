import React from 'react';
import Paper from '@material-ui/core/Paper';
import {withStyles} from '@material-ui/core/styles';

import VirtualizedTable from './VirtualizedTable.jsx';
import SearchBar from './SearchBar.jsx';

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

const sample = [
	['Frozen yoghurt', 159, 6.0, 24, 4.0],
	['Ice cream sandwich', 237, 9.0, 37, 4.3],
	['Eclair', 262, 16.0, 24, 6.0],
	['Cupcake', 305, 3.7, 67, 4.3],
	['Gingerbread', 356, 16.0, 49, 3.9],
];

function createData(id, dessert, calories, fat, carbs, protein) {
	return {id, dessert, calories, fat, carbs, protein};
}

const rows = [];

for (let i = 0; i < 2000; i += 1) {
	const randomSelection = sample[Math.floor(Math.random() * sample.length)];
	rows.push(createData(i, ...randomSelection));
}

function MuiReactVirtualizedTable(props) {
	const {classes, placeholder, ariaLabel} = props;
	const [search, setSearch] = React.useState('');

	const searchValue = () => {
		console.log('click');
		if (!search) return;
		console.log(search);
	};

	return (
		<Paper className={classes.root}>
			<SearchBar
				placeholder={placeholder}
				ariaLabel={ariaLabel}
				value={search}
				onChange={(searchVal) => setSearch(searchVal)}
				onSearch={() => searchValue(search)}
				onClear={() => setSearch('')}
			/>
			<Paper className={classes.tableContainer}>
				<VirtualizedTable
					rowCount={rows.length}
					rowGetter={({index}) => rows[index]}
					columns={[
						{
							width: 200,
							label: 'Dessert',
							dataKey: 'dessert',
						},
						{
							width: 120,
							label: 'Calories\u00A0(g)',
							dataKey: 'calories',
							numeric: true,
						},
						{
							width: 120,
							label: 'Fat\u00A0(g)',
							dataKey: 'fat',
							numeric: true,
						},
						{
							width: 120,
							label: 'Carbs\u00A0(g)',
							dataKey: 'carbs',
							numeric: true,
						},
						{
							width: 120,
							label: 'Protein\u00A0(g)',
							dataKey: 'protein',
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
