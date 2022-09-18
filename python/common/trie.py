class Trie:
  def __init__(self):
    self.is_final = False
    self.children = [None] * 26

  def insert(self, word, i):
    if i == len(word):
      self.is_final = True
      return

    k = ord(word[i]) - ord('a')
    if self.children[k] == None:
      self.children[k] = Trie()

    self.children[k].insert(word, i + 1)

  def search(self, word, i):
    if i == len(word):
      return self.is_final

    k = ord(word[i]) - ord('a')

    if self.children[k] == None:
      return False
    return self.children[k].search(word, i + 1)
