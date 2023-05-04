import './app.css'
import { createInertiaApp } from '@inertiajs/svelte'
import Layout from './Shared/Layout.svelte'

const teamNoLayout = ['Auth/Login']

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
			layout: teamNoLayout.includes(name) ? undefined : Layout,
		}
	},
	setup({ el, App, props }) {
		new App({ target: el, props })
	},
})
