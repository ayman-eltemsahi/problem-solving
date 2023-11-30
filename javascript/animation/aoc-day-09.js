const { lines } = require("./input");

const WIDTH = 60;
const HEIGHT = 30;
const MARGIN_TOP = 2;
const abs = Math.abs;
const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms));
const printAt = (x, y, ...args) => {
  process.stdout.write(`\x1b[${x};${y}f`);
  process.stdout.write(...args);
};

const drawEmptyGrid = () => {
  process.stdout.write("\033c");

  for (let i = -HEIGHT; i <= HEIGHT; i++)
    for (let j = -WIDTH; j <= WIDTH; j++) printAt(i + HEIGHT + MARGIN_TOP, j + WIDTH, ".");
};

const clearKnots = (knots) => {
  for (const [x, y] of knots) printAt(x + HEIGHT + MARGIN_TOP, y + WIDTH, ".");
};

const rawKnots = (knots) => {
  for (let i = 9; i > 0; i--) {
    printAt(knots[i][0] + HEIGHT + MARGIN_TOP, knots[i][1] + WIDTH, `\x1b[32m${i}\x1b[0m`);
  }

  printAt(knots[0][0] + HEIGHT + MARGIN_TOP, knots[0][1] + WIDTH, `\x1b[32m${"H"}\x1b[0m`);
};

const knots = Array(10)
  .fill(0)
  .map(() => [0, 0]);
const steps = { U: [0, -1], D: [0, 1], L: [-1, 0], R: [1, 0] };

const move = (dir, parent, child) => {
  const s = steps[dir];
  if (parent === 0) {
    knots[parent][0] += s[0];
    knots[parent][1] += s[1];
  }

  const dx = knots[parent][0] - knots[child][0];
  const dy = knots[parent][1] - knots[child][1];

  if (abs(dx) == 0 && abs(dy) > 1) {
    knots[child][1] += Math.floor(dy / abs(dy));
  } else if (abs(dy) == 0 && abs(dx) > 1) {
    knots[child][0] += Math.floor(dx / abs(dx));
  } else if (abs(dx) > 1 || abs(dy) > 1) {
    knots[child][0] += Math.floor(dx / abs(dx));
    knots[child][1] += Math.floor(dy / abs(dy));
  }
};

const run = async () => {
  let x = 0;
  for (const line of lines) {
    x++;
    let [dir, cnt] = line.split(" ");
    cnt = Number.parseInt(cnt);

    if (dir === "D" && knots[0][1] + cnt > WIDTH) dir = "U";
    if (dir === "R" && knots[0][0] + cnt > HEIGHT) dir = "L";

    while (cnt-- > 0) {
      const prev = knots.map((a) => a.slice());
      for (let i = 0; i < 9; i++) {
        move(dir, i, i + 1);
      }

      clearKnots(prev);
      rawKnots(knots);
      printAt(0, 0, " ");
      await delay(x < 15 ? 200 : x < 50 ? 50 : 10);
    }
  }
};

drawEmptyGrid();
run()
  .catch(console.error)
  .then(() => {
    printAt(HEIGHT + HEIGHT, WIDTH + WIDTH, " ");
  });
