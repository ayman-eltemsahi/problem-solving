#pragma once
#include <algorithm>
#include <numeric>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <stack>
#include <queue>
#include <queue>
#include <unordered_map>
#include <unordered_set>
#include <map>
#include <set>

#include "common.hpp"
#include "utils/print-vector.hpp"
#include "utils/read-vector.hpp"
#include "utils/input-reader.hpp"
#include "utils/tree.hpp"
#include "utils/expect.hpp"

typedef long long int ll;
typedef std::vector<int> vi;
using std::abs;
using std::cout;
using std::greater;
using std::map;
using std::max;
using std::min;
using std::multimap;
using std::multiset;
using std::pair;
using std::priority_queue;
using std::queue;
using std::set;
using std::stack;
using std::string;
using std::swap;
using std::to_string;
using std::unordered_map;
using std::unordered_set;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define DEBUG(a) std::cout << #a << ": " << (a) << "\n"
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
#define LOG4(a, b, c, d) std::cout << (a) << ", " << (b) << ", " << (c) << ", " << (d) << "\n"

// const int mod = (int)(1e9 + 7);
