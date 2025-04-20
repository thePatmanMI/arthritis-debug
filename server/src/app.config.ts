import config from "@colyseus/tools";

import { WebSocketTransport } from "@colyseus/ws-transport";
import { monitor } from "@colyseus/monitor";
import { playground } from "@colyseus/playground";

/**
 * Import your Room files
 */
import { ArthritisRoom } from "./rooms/ArthritisRoom";

export default config({
	options: {
		devMode: true,
	},

	initializeTransport: (options) => new WebSocketTransport({
		pingInterval: 0,  // this is to allow debug to not timeout
		...options
	}),

	initializeGameServer: (gameServer) => {
		/**
			* Define your room handlers:
			*/
		gameServer.define('game', ArthritisRoom);

	},

	initializeExpress: (app) => {
		/**
			* Bind your custom express routes here:
			*/
		app.get("/", (req, res) => {
				res.send(`Instance ID => ${process.env.NODE_APP_INSTANCE ?? "NONE"}`);
		});

		/**
			* Bind @colyseus/monitor
			* It is recommended to protect this route with a password.
			* Read more: https://docs.colyseus.io/tools/monitor/
			*/
		app.use("/colyseus", monitor());

		// Bind "playground"
		app.use("/playground", playground);

	},


	beforeListen: () => {
			/**
				* Before before gameServer.listen() is called.
				*/
		}
});
