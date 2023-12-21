cmake_minimum_required(VERSION 3.21)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_BUILD_TYPE Release)
set(CMAKE_CXX_FLAGS_RELEASE "-O3")

add_definitions(-DRUNNING_LOCALLY)
# add_definitions(-DBENCHMARK)


project(problem-solving)

include_directories("leetcode")
include_directories("advent-of-code")

list(APPEND INPUT_FILES
  "hackerrank/utils.hpp"
  "hackerrank/utils.cpp"

  # "playground/test.cpp"

  # "fb/round2/valet-parking.cpp"
  # "hackerrank/extremum-permutations.cpp"


  "advent-of-code/2023/day-17.cpp"

  # "leetcode/making-file-names-unique.cpp"
  # "leetcode/playground.cpp"
)

add_executable (binary ${INPUT_FILES} )


# add_custom_command(
#     TARGET binary
#     COMMAND cat input | build/binary
#     WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
#     COMMENT "Running the code"
# )
