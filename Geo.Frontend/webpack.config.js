const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
	mode: process.env.NODE_ENV === 'production' ? 'production' : 'development',
	entry: './src/main.js',
	output: {
		path: path.resolve(__dirname, 'wwwroot'),
		filename: 'index.bundle.js'
	},
	plugins: [
		new HtmlWebpackPlugin()
	],
	module: {
		rules: [
			{ test: /\.svg$/, use: 'svg-inline-loader' },
			{ test: /\.css$/, use: [ 'style-loader', 'css-loader' ] },
			{ test: /\.(js)$/, use: 'babel-loader' }
		]
	},
}