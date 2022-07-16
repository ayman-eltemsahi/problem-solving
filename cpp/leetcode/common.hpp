struct TreeNode {
  int val;
  TreeNode* left;
  TreeNode* right;
  TreeNode() : val(0), left(nullptr), right(nullptr) {
  }
  TreeNode(int x) : val(x), left(nullptr), right(nullptr) {
  }
  TreeNode(int x, TreeNode* left, TreeNode* right) : val(x), left(left), right(right) {
  }
};

struct ListNode {
  int val;
  ListNode* next;
  ListNode() : val(0), next(nullptr) {
  }
  ListNode(int x) : val(x), next(nullptr) {
  }
  ListNode(int x, ListNode* next) : val(x), next(next) {
  }
};

class Trie {
 public:
  bool is_final;
  Trie* children[26];

  Trie() {
    this->is_final = false;
    memset(this->children, 0, sizeof(this->children));
  }

  void insert(std::string word) {
    insert_internal(word, 0);
  }

  bool search(std::string word) {
    return this->search_internal(word, 0, true);
  }

  bool startsWith(std::string prefix) {
    return this->search_internal(prefix, 0, false);
  }

 private:
  void insert_internal(const std::string& word, int i) {
    if (i == word.length()) {
      this->is_final = true;
      return;
    }

    if (!this->children[word[i] - 'a']) {
      this->children[word[i] - 'a'] = new Trie();
    }

    this->children[word[i] - 'a']->insert_internal(word, i + 1);
  }

  bool search_internal(const std::string& word, int i, bool search_full_word) {
    if (i == word.length()) {
      return search_full_word ? this->is_final : true;
    }

    if (!this->children[word[i] - 'a']) return false;
    return this->children[word[i] - 'a']->search_internal(word, i + 1, search_full_word);
  }
};
