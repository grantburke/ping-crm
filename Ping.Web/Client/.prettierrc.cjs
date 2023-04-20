module.exports = {
	tabWidth: 3,
	useTabs: true,
	semi: false,
	singleQuote: true,
	tailwindConfig: './tailwind.config.cjs',
	svelteBracketNewLine: false,
	pluginSearchDirs: ['.'],
	overrides: [
		{
			files: '*.svelte',
			options: {
				parser: 'svelte',
			},
		},
	],
}
