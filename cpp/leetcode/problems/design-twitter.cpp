#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <unordered_set>
#include <unordered_map>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

class Tweet {
 public:
  int id;
  int time;
  Tweet* next;
  Tweet(int id_, int time_, Tweet* next_) : id(id_), time(time_), next(next_) {
  }
};

class Twitter {
 public:
  int time = 0;
  std::unordered_map<int, std::unordered_set<int>> followed_users;
  std::unordered_map<int, Tweet*> users;
  std::unordered_map<int, int> tweets_time;

  Twitter() {
  }

  void postTweet(int userId, int tweetId) {
    this->time++;
    tweets_time[tweetId] = this->time;
    if (users.find(userId) == users.end()) {
      users[userId] = new Tweet(tweetId, this->time, nullptr);
      return;
    }

    Tweet* curr = users[userId];
    Tweet* tweet = new Tweet(tweetId, this->time, curr);
    users[userId] = tweet;
  }

  vector<int> getNewsFeed(int userId) {
    vector<int> res;
    res.reserve(20);

    add_user_tweets(userId, res, 10);
    auto tweets_time_ref = &tweets_time;
    for (auto user : followed_users[userId]) {
      add_user_tweets(user, res, 10);
      std::sort(res.begin(), res.end(), [tweets_time_ref](int a, int b) {
        return (*tweets_time_ref)[a] > (*tweets_time_ref)[b];
      });

      if (res.size() > 10) res.erase(res.begin() + 10, res.end());
    }

    return res;
  }

  void add_user_tweets(int userId, vector<int>& res, int c) {
    if (users.find(userId) == users.end()) return;
    auto head = users[userId];
    while (c-- && head) {
      LOG(c);
      res.push_back(head->id);
      head = head->next;
    }
  }

  void follow(int followerId, int followeeId) {
    followed_users[followerId].insert(followeeId);
  }

  void unfollow(int followerId, int followeeId) {
    if (followed_users.find(followerId) == followed_users.end()) return;
    followed_users[followerId].erase(followeeId);
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Twitter t;
  t.postTweet(1, 1000);
  auto res = t.getNewsFeed(1);
  t.follow(1, 2);
  t.unfollow(1, 2);
}
#endif
