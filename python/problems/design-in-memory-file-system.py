from local_stuff import *


class Node:
  def __init__(self, path, type, contents=None):
    self.path = path
    self.type = type
    self.children = {}
    self.contents = contents

  def getChild(self, name):
    return None if name not in self.children else self.children[name]

  def addChild(self, name, value):
    self.children[name] = value

class FileSystem:

    def __init__(self):
      self.root = Node('', 'DIR')

    def ls(self, path: str) -> List[str]:
      node = self.get(path)

      if node.type == 'FILE':
        return [node.path]
      else:
        return sorted(list(node.children))


    def mkdir(self, path: str) -> None:
      node = self.root
      for part in filter(lambda x: x, path.split('/')):
        tmp = node.getChild(part)
        if not tmp:
          tmp = Node(part, 'DIR')
          node.addChild(part, tmp)

        node = tmp


    def addContentToFile(self, filePath: str, content: str) -> None:
      dirPath = filePath.split('/')
      filename = dirPath.pop()
      dirPath = '/'.join(dirPath)
      self.mkdir(dirPath)
      node = self.get(dirPath)

      file = node.getChild(filename)
      if not file:
        file = Node(filename, 'FILE', '')
        node.addChild(filename, file)

      file.contents += content

    def readContentFromFile(self, filePath: str) -> str:
      return self.get(filePath).contents


    def get(self, path: str) -> Node:
      node = self.root
      for part in filter(lambda x: x, path.split('/')):
        node = node.getChild(part)
      return node

