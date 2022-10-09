#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include "common.hpp"

using std::pair;
using std::string;
using std::vector;

string NLL = "\U0001f525";
string NEG = "❌";
string POS = "✅";
vector<string> numbers{"0️⃣", "1️⃣", "2️⃣", "3️⃣", "4️⃣",
                       "5️⃣", "6️⃣", "7️⃣", "8️⃣", "9️⃣"};
int NUM_LEN = string("0️⃣").length();

int getIndex(const string& k) {
  for (int i = 0; i < numbers.size(); i++)
    if (k == numbers[i]) return i;
  return -1;
}

string convert(int n) {
  string k = "";
  int r = abs(n);
  while (r) {
    k = numbers[r % 10] + k;
    r /= 10;
  }

  return (n < 0 ? NEG : POS) + k;
}

int parse(string& k, int& i) {
  int neg = k.substr(i, NEG.length()) == NEG ? -1 : 1;
  i += NEG.length();
  int n = 0;
  while (true) {
    int index = getIndex(k.substr(i, NUM_LEN));
    if (index > -1) {
      i += NUM_LEN;
      n = n * 10 + index;
    } else
      break;
  }

  return n * neg;
}

bool isNull(string& k, int i) {
  return k.substr(i, NLL.length()) == NLL;
}

class Codec {
 public:
  // Encodes a tree to a single string.
  string serialize(TreeNode* root) {
    auto res = serialize_internal(root);
    std::cout << res << "\n";
    return res;
  }

  string serialize_internal(TreeNode* root) {
    if (!root) return NLL;
    return convert(root->val) + serialize_internal(root->left) + serialize_internal(root->right);
  }

  // Decodes your encoded data to tree.
  TreeNode* deserialize(string data) {
    int i = 0;
    return deserialize_internal(data, i);
  }

  TreeNode* deserialize_internal(string& data, int& i) {
    if (isNull(data, i)) {
      i += NLL.length();
      return nullptr;
    }

    int val = parse(data, i);
    auto left = deserialize_internal(data, i);
    auto right = deserialize_internal(data, i);

    return new TreeNode(val, left, right);
  }
};
