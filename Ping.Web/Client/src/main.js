import './app.css'
import { createInertiaApp } from '@inertiajs/svelte'
import Layout from './Shared/Layout.svelte'

createInertiaApp({
	progress: {
		// color: 'rgb(var(--color-bg-900))',
		includeCSS: true,
	},
	resolve: (name) => {
		const pages = import.meta.glob('./Pages/**/*.svelte', { eager: true })
		let page = pages[`./Pages/${name}.svelte`]
		return {
			default: page.default,
			layout: page.layout || Layout,
		}
	},
	setup({ el, App, props }) {
		new App({ target: el, props })
	},
})
