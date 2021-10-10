cmake_minimum_required(VERSION 3.21)

set(CMAKE_CXX_STANDARD 14)
add_definitions(-DRUNNING_LOCALLY)


project(problem-solving)


list(APPEND INPUT_FILES
  "hackerrank/utils.hpp"
  "hackerrank/utils.cpp"

  # "playground/test.cpp"

  # "fb/round2/valet-parking.cpp"
  # "hackerrank/extremum-permutations.cpp"


  "codeforces/gamer-hemose.cpp"

  # "leetcode/making-file-names-unique.cpp"
  # "leetcode/playground.cpp"
)

add_executable (binary ${INPUT_FILES})


# add_custom_command(
#     TARGET binary
#     COMMAND cat input | build/binary
#     WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
#     COMMENT "Running the code"
# )
