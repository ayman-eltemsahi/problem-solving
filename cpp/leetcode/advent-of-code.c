#include <fcntl.h>
#include <sys/mman.h>
#include <unistd.h>
#include <stdio.h>

int fallocate(int fd, int len) {
  fstore_t store = {F_ALLOCATECONTIG, F_PEOFPOSMODE, 0, len};
  int ret = fcntl(fd, F_PREALLOCATE, &store);
  if (-1 == ret) {
    // OK, perhaps we are too fragmented, allocate non-continuous
    store.fst_flags = F_ALLOCATEALL;
    ret = fcntl(fd, F_PREALLOCATE, &store);
  }
  ftruncate(fd, len);
  return 1;
}

int main() {
  char* filename = "/Users/ayman.eltemmsahy/Downloads/aoc_2022_day01_large_input.txt";
  int file_read = open(filename, O_RDONLY, 0);
  off_t fsize = lseek(file_read, 0, SEEK_END);

  fallocate(file_read, fsize);
  // posix_fallocate(file_read, 0, fsize);

  char* buffer = (char*)(mmap(NULL, fsize, PROT_READ, MAP_SHARED, file_read, 0));
  madvise(buffer, fsize, MADV_SEQUENTIAL);
  char* end = buffer + fsize;

  int a = 0, b = 0, c = 0, cur = 0;
  while (buffer != end) {
    if (*buffer == '\n') {
      // ==================
      if (cur > a) {
        c = b;
        b = a;
        a = cur;
      } else if (cur > b) {
        c = b;
        b = cur;
      } else if (cur > c) {
        c = cur;
      }
      // ==================

      cur = 0;
    } else {
      int r = 0;
      while (*buffer != '\n') {
        r = r * 10 + (*buffer - '0');
        buffer++;
      }

      cur += r;
    }

    buffer++;
  }

  printf("\033[1;32mTop 3: %d %d %d \n\033[0m", a, b, c);
  printf("\033[1;32mSum : %d \n\033[0m", a + b + c);
  close(file_read);
}
