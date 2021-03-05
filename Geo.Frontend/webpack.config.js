const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const webpack = require('webpack');

module.exports = {
	mode: 'production',
	entry: './src/main.js',
	output: {
		path: path.resolve(__dirname, 'wwwroot'),
		filename: '[name].[contenthash].bundle.js',
		clean: true,
	},
	plugins: [
		new webpack.ProgressPlugin(),
		new HtmlWebpackPlugin({
			title: 'Geo Information',
			template: path.resolve(__dirname, './src/index.html'),
			filename: 'index.html',
			minify: false
		})
	],
	module: {
		rules: [
			{test: /\.svg$/, use: 'svg-inline-loader'},
			{test: /\.css$/, use: ['style-loader', 'css-loader']},
			{test: /\.(js)$/, use: 'babel-loader'}
		]
	},
	optimization: {
		moduleIds: 'deterministic',
		runtimeChunk: 'single',
		splitChunks: {
			cacheGroups: {
				vendor: {
					test: /[\\/]node_modules[\\/]/,
					name: 'vendors',
					chunks: 'all',
				},
			},
		},
	},
}