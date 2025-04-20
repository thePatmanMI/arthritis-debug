import assert from "assert";
import { boot } from "@colyseus/testing";

// import your "app.config.ts" file here.
import appConfig from "../src/app.config.js";

describe("testing your Colyseus app", () => {
  let colyseus;

  before(async () => colyseus = await boot(appConfig));
  after(async () => colyseus.shutdown());

  beforeEach(async () => await colyseus.cleanup());

  it("connecting into a room", async () => {
    // `room` is the server-side Room instance reference.
    const room = await colyseus.createRoom("game", {});

    // `client1` is the client-side `Room` instance reference (same as JavaScript SDK)
    const client1 = await colyseus.connectTo(room);

    // make your assertions
    assert.strictEqual(client1.sessionId, room.clients[0].sessionId);

    // wait for state sync
    await room.waitForNextPatch();

    var expected = {
      deck: [],
      discardPile: [],
      playerOrder: [
        client1.sessionId
      ],
      players: {},
      round: {
        currentRound: "1",
        objectives: [
          "set of 3",
          "set of 3",
          "discard"
        ],
        startingHandSize: 7
      }
    };

    assert.deepStrictEqual(client1.state.toJSON(), expected);
  });
});