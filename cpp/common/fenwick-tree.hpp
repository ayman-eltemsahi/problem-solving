#pragma once

template <class T>
class FenwickTree {
 private:
  int size;
  vector<T> tree;

 public:
  FenwickTree(int sz) : size(sz), tree(sz + 1) {}

  T getSum(int index) {
    int sum = 0;
    index++;

    while (index > 0) {
      sum += tree[index];
      index -= index & (-index);
    }
    return sum;
  }

  void update(int index, T val) {
    index++;
    while (index <= size) {
      tree[index] += val;
      index += index & (-index);
    }
  }
};
