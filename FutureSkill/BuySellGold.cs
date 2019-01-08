using System;
using System.Linq;

static class API
{
    static Random r = new Random();
    static int[] nums = new int[] { 5, 4, 4, 1, 4, 6, 6, 7, 8, 6 };
    public static int GetNumDays()
    {
        return nums.Length;
    }
    public static int GetPriceOnDay(int i)
    {
        return nums[i];
    }
}

public class Solution
{
    int n, buyDay, sellDay;
    long[] prices;
    int[] maxIndex;
    public Solution()
    {
        Console.WriteLine("start");
        n = API.GetNumDays();

        prices = new long[n];
        maxIndex = new int[n];

        for (int day = 0; day < n; day++)
        {
            prices[day] = API.GetPriceOnDay(day);
        }

        maxIndex[n - 1] = n - 1;
        for (int day = n - 2; day >= 0; day--)
        {

            maxIndex[day] = (prices[day] > prices[maxIndex[day + 1]]) ? day : maxIndex[day + 1];
        }

        long maxProfit = long.MinValue;
        for (int buy = 0; buy < n - 1; buy++)
        {
            int sell = maxIndex[buy + 1];
            long currentProfit = prices[sell] - prices[buy];
            if (currentProfit > maxProfit)
            {
                maxProfit = currentProfit;
                buyDay = buy;
                sellDay = sell;
            }
        }
    }

    /**
     * Return the day which you buy gold. The first day has number zero. This 
     * method is called first, and only once.
     */
    public int GetBuyDay()
    {
        return buyDay;
    }

    /**
     * Return the day to sell gold on. This day has to be after (greater than) 
     * the buy day. The first day has number zero (although this is not a valid 
     * sell day). This method is called second, and only once.
     */
    public int GetSellDay()
    {
        return sellDay;
    }
}
