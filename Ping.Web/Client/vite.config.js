import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [svelte()],
	build: {
		// generate manifest.json in outDir
		manifest: true,
		rollupOptions: {
			// overwrite default .html entry
			input: './src/main.js',
		},
		outDir: '../wwwroot',
	},
	server: {
		origin: 'http://localhost:5173',
		proxy: {
			'*': {
				target: 'http://localhost:5240',
				changeOrigin: true,
			},
		},
		hmr: {
			protocol: 'ws',
		},
	},
})
