class Trie {
  private boolean is_final;
  private Trie[] children;

  public Trie() {
    is_final = false;
    children = new Trie[26];
  }

  void insert(String word, int i) {
    if (i == word.length()) {
      is_final = true;
      return;
    }

    if (children[word.charAt(i) - 'a'] == null) {
      children[word.charAt(i) - 'a'] = new Trie();
    }

    children[word.charAt(i) - 'a'].insert(word, i + 1);
  }

  boolean search(String word, int i) {
    if (i == word.length())
      return is_final;

    if (children[word.charAt(i) - 'a'] == null)
      return false;
    return children[word.charAt(i) - 'a'].search(word, i + 1);
  }
};
