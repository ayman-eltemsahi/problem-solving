const fs = require("fs");

const read = () => {
  const lines = fs.readFileSync("./input.txt").toString().split("\n");
  if (!lines[lines.length - 1]) {
    lines.pop();
  }

  return lines;
};

const lines = read();

module.exports = { lines, read };
