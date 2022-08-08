#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

typedef unsigned long long int ull;
class Bitset {
 public:
  vector<ull> v;
  int size, ones;
  bool flipped;
  Bitset(int size) {
    this->size = size;
    this->ones = 0;
    this->flipped = false;
    this->v = vector<ull>(size / 64 + 1);
  }

  void fix(int idx) {
    if (flipped) {
      if (this->unfix_util(idx)) this->ones++;
    } else {
      if (this->fix_util(idx)) this->ones++;
    }
  }

  void unfix(int idx) {
    if (flipped) {
      if (this->fix_util(idx)) this->ones--;
    } else {
      if (this->unfix_util(idx)) this->ones--;
    }
  }

  bool fix_util(int idx) {
    int a = idx / 64;
    int b = idx % 64;
    if (((v[a] >> b) & 1) == 1) return false;
    v[a] |= (1L << b);
    return true;
  }

  bool unfix_util(int idx) {
    int a = idx / 64;
    int b = idx % 64;
    if (((v[a] >> b) & 1) == 0) return false;
    v[a] &= ~(1UL << b);
    return true;
  }

  void flip() {
    flipped = !flipped;
    this->ones = this->size - this->ones;
  }

  bool all() {
    return this->ones == this->size;
  }

  bool one() {
    return this->ones > 0;
  }

  int count() {
    return this->ones;
  }

  string toString() {
    vector<char> ss(size);
    for (int i = 0; i < size; i++) {
      int a = i / 64;
      int b = i % 64;
      if (((v[a] >> b) & 1) == 1) {
        ss[i] = (flipped ? '0' : '1');
      } else {
        ss[i] = (flipped ? '1' : '0');
      }
    }

    return string(ss.begin(), ss.end());
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Bitset bitset(5);
  bitset.fix(1);
  LOG(bitset.toString());
}
#endif
