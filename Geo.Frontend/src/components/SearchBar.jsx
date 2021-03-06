import React, {useEffect, useRef, useState} from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import InputBase from '@material-ui/core/InputBase';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import SearchIcon from '@material-ui/icons/Search';
import ClearIcon from '@material-ui/icons/Clear';

const useStyles = makeStyles((theme) => ({
	root: {
		padding: '2px 4px',
		display: 'flex',
		alignItems: 'center',
		width: '100%',
		zIndex: 1
	},
	input: {
		marginLeft: theme.spacing(1),
		flex: 1,
	},
	iconButton: {
		padding: 10,
	},
	divider: {
		height: 28,
		margin: 4,
	},
}));

const SearchBar = (props) => {
	const classes = useStyles();
	const [clear, setClear] = useState(false);
	const searchInput = useRef(null);
	const { placeholder, ariaLabel } = props;

	useEffect(() => {
		setClear(!!searchInput.current?.value);
	}, [props.value])

	const onSearch = () => {
		props.onSearch();
	}

	const onChange = () => {
		props.onChange(searchInput.current?.value);
	}

	const keyPress = (e) => {
		if (e.code === 'Enter' || e.charCode === 13) {
			e.preventDefault()
			props.onSearch();
		}
	}

	const onClear = () => {
		const current = searchInput.current;
		if (current) {
			current.value = '';
		}
		props.onClear();
	}

	return (
		<Paper component="form" className={classes.root}>
			<InputBase
				className={classes.input}
				placeholder={placeholder}
				inputProps={{'aria-label': ariaLabel}}
				onKeyPress={keyPress}
				defaultValue={''}
				inputRef={searchInput}
				onChange={onChange}
			/>
			<IconButton type="button" className={classes.iconButton} aria-label="поиск"
						onClick={onSearch}>
				<SearchIcon/>
			</IconButton>
			{
				clear && (
					<>
						<Divider className={classes.divider} orientation="vertical"/>
						<IconButton type="button" color="primary" className={classes.iconButton} aria-label="отчистить"
									onClick={onClear}>
							<ClearIcon/>
						</IconButton>
					</>
				)
			}

		</Paper>
	);
}

export default SearchBar;