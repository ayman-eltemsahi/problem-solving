#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Solution {
  string justify_line(vector<string>& line_words, int maxWidth) {
    int rem = 0;
    for (auto& w : line_words) rem += w.length();
    rem = maxWidth - rem;

    string line = line_words[0];
    int spaces = line_words.size() - 1;
    int j = 1;
    for (; j < line_words.size() && j <= (rem % spaces); j++)
      line += string(rem / spaces + 1, ' ') + line_words[j];

    for (; j < line_words.size(); j++) line += string(rem / spaces, ' ') + line_words[j];

    return line + string(maxWidth - line.length(), ' ');
  }

 public:
  vector<string> fullJustify(vector<string>& words, int maxWidth) {
    if (!words.size()) return {};
    vector<vector<string>> lines;

    int i = 0;
    while (i < words.size()) {
      lines.push_back({});
      lines.back().push_back(words[i]);
      int width = words[i++].length();
      while (i < words.size() && width + 1 + words[i].length() <= maxWidth) {
        lines.back().push_back(words[i]);
        width += 1 + words[i++].length();
      }
    }

    int n = lines.size();
    vector<string> res(n);
    for (int i = 0; i < n - 1; i++) res[i] = justify_line(lines[i], maxWidth);

    auto& line_words = lines[n - 1];
    auto last_line = line_words[0];
    for (int i = 1; i < line_words.size(); i++) last_line += " " + line_words[i];
    res[n - 1] = last_line + string(maxWidth - last_line.length(), ' ');

    return res;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  Input input;
  auto words = input.next_vector_string();
  auto width = input.next_int();
  auto res = s.fullJustify(words, width);
  for (auto r : res) LOG(r + "___");
}
#endif
