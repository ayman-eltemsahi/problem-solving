
import java.util.Arrays;
import java.util.List;

public class ChangeMachine {

  public static Integer countChange(Integer money, List<Integer> coins,
                                    Integer arrLength) {

    Integer[] count = new Integer[money + 1];
    Arrays.fill(count, 0);
    count[0] = 1;

    for (int i = 0; arrLength > i; i++)
      for (int j = coins.get(i); j <= money; j++)
        count[j] += count[j - coins.get(i)];
    return count[money];
  }

  public static void main(String[] args) {

    Integer arr[] = {1,  2, 3,  1, 2,  31, 2,  31, 2,
                     31, 2, 31, 2, 31, 2,  31, 2,  3};
    Integer arrLength = arr.length;
    Integer money = 1000000;
    System.out.println(countChange(money, Arrays.asList(arr), arrLength));
  }
}
