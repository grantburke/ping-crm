/** @type {import('tailwindcss').Config} */
export default {
	content: ['../Views/**/*.cshtml', './src/app.css', './src/**/*.{svelte,js}'],
	darkMode: 'class',
	theme: {
		extend: {},
	},
	plugins: [
		require('@tailwindcss/forms'),
		require('prettier-plugin-tailwindcss'),
	],
}
