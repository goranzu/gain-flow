import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from "@tailwindcss/vite";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
	server: {
		proxy: {
			"/api": {
				target: 'http://localhost:5000',
				changeOrigin: true,
				// configure: (proxy, options) => {
				// 	proxy.on('proxyReq', (proxyReq, req, res) => {
				// 		console.log('Proxying request:', req.method, req.url, '-> ', options.target + req.url);
				// 	});
				// },
			},
		}
	}
})