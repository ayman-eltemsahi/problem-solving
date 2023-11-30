const INPUT = require("./input");

const MARGIN_TOP = 30;
const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms));

const getStacks = () => {
  const lines = [];
  for (const line of INPUT.split("\n")) {
    if (!line) break;
    lines.push(line);
  }

  const m = Number.parseInt(lines.pop().trim().split(" ").reverse()[0]);
  const n = lines.length;
  const stacks = Array(m)
    .fill(0)
    .map(() => Array(n).fill(""));

  for (let i = 0; i < n; i++) {
    for (let j = 0; j < m; j++) {
      stacks[j][n - i - 1] = lines[i].substring(j * 4, (j + 1) * 4).trim();
    }
  }

  for (let i = 0; i < n; i++) {
    while (!stacks[i][stacks[i].length - 1]) stacks[i].pop();
  }

  return stacks;
};

const getOps = () => {
  const lines = INPUT.split("\n");
  let i = 0;
  while (lines[i]) i++;
  i++;
  const ops = [];
  while (i < lines.length) {
    if (!lines[i]) break;
    const op = lines[i].replace("move ", "").replace("from ", "").replace("to ", "").trim().split(" ");
    ops.push({ count: parseInt(op[0]), from: parseInt(op[1]) - 1, to: parseInt(op[2]) - 1 });
    i++;
  }
  return ops;
};

const printAt = (x, y, ...args) => {
  process.stdout.write(`\x1b[${x};${y}f`);
  process.stdout.write(...args);
};

const printStacks = (stacks) => {
  process.stdout.write("\033c");

  const n = stacks.length;
  const m = Math.max(...stacks.map((s) => s.length));
  for (let j = m - 1; j >= 0; j--) {
    for (let i = 0; i < n; i++) {
      printAt(MARGIN_TOP + m - j + 4, (i + 1) * 5, stacks[i][j] ? stacks[i][j] + " " : "    ");
    }
  }
};

const stacks = getStacks();
const ops = getOps();

const getMaxElevation = (from, to) => {
  if (from > to) return getMaxElevation(to, from);
  let ans = stacks[from].length;
  while (from <= to) {
    ans = Math.max(ans, stacks[from].length);
    from++;
  }
  return ans;
};

const run = async () => {
  printStacks(stacks);
  const m = Math.max(...stacks.map((s) => s.length));

  const p = (i, j, ...args) => printAt(MARGIN_TOP + m - j + 4, (i + 1) * 5, ...args);

  for (const op of ops) {
    let { count, from, to } = op;
    const myOp = count === 3 && from === 6 && to === 4 ? 1 : 0;

    while (count-- > 0) {
      printAt(0, 0, `from ${from + 1} to ${to + 1}`);

      const top = getMaxElevation(from, to);
      const segments = [
        {
          from: { x: from, y: stacks[from].length - 1 },
          to: { x: from, y: top },
        },
        {
          from: { x: from, y: top },
          to: { x: to, y: top },
        },
        {
          from: { x: to, y: top },
          to: { x: to, y: stacks[to].length },
        },
      ];

      const val = stacks[from].pop();
      if (!val.trim()) {
        throw new Error(val);
      }

      let x, y;
      for (const seg of segments) {
        const { from, to } = seg;
        const dx = Math.sign(to.x - from.x);
        const dy = Math.sign(to.y - from.y);
        x = from.x;
        y = from.y;
        y;
        do {
          await delay(10);
          p(x, y, "   ");
          x += dx;
          y += dy;
          p(x, y, `\x1b[32m${val}\x1b[0m`);
        } while (x !== to.x || y !== to.y);
      }
      p(x, y, val);

      stacks[to].push(val);
    }
  }

  printAt(stacks.length + MARGIN_TOP + 2, 0, "\n");
};

run().catch(console.error);
