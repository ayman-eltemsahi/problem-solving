# Trie
class Trie
  attr_accessor :is_final, :children

  def initialize
    @is_final = false
    @children = Array.new(26)
  end

  def insert(word, i)
    if i == len(word)
      self.is_final = true
      return
    end

    k = word[i].ord - 'a'.ord
    children[k] = Trie.new if children[k].nil?

    children[k].insert(word, i + 1)
  end

  def search(word, i)
    return @is_final if i == word.length

    k = word[i].ord - 'a'.ord
    return false if @children[k].nil?

    @children[k].search(word, i + 1)
  end
end
