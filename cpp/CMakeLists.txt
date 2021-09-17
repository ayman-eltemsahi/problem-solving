cmake_minimum_required(VERSION 3.21)

set(CMAKE_CXX_STANDARD 14)

project(problem-solving)


list(APPEND INPUT_FILES
  "hackerrank/utils.hpp"
  "hackerrank/utils.cpp"


  # "fb/round1/chapter1.cpp"
  # "fb/round1/chapter2.cpp"
  "fb/round1/chapter3.cpp"

  # "hackerrank/cloudy-day.cpp"
  # "hackerrank/roads-in-hackerland.cpp"
  # "hackerrank/maximum-palindromes.cpp"
  # "hackerrank/synchronous-shopping.cpp"
)

add_executable (binary ${INPUT_FILES})


# add_custom_command(
#     TARGET binary
#     COMMAND cat input | build/binary
#     WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
#     COMMENT "Running the code"
# )
