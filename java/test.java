import java.io.File;
import java.io.FileNotFoundException; // Import this class to handle errors
import java.util.*;
import java.util.Scanner;

public class test {

  public static void main(String[] args) {
    Scanner sc = getFileScanner("./input");

    char[] wordArray = sc.nextLine().toLowerCase().toCharArray();

    Set<Character> hs = new HashSet<>();
    Boolean flag = true;
    for (int i = 0; i < wordArray.length; i++) {
      if (hs.contains(wordArray[i])) {
        flag = false;
        break;
      }
      hs.add(wordArray[i]);
    }

    System.out.println(flag ? "Yes" : "No");
  }

  public static Scanner getFileScanner(String file) {
    try {
      return new Scanner(new File(file));
    } catch (FileNotFoundException e) {
      System.out.println("An error occurred.");
      e.printStackTrace();
      return null;
    }
  }
}
