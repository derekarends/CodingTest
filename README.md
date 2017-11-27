# CodingTest
Testing:
* Start Solution
* POST {host}/api/v1/triangle
  * Creates the inital triangle layout
* GET {host}/api/v1/triangle
  * Returns all triangles
* GET {host}/api/v1/triangle/v1/10,20/v2/10,30/v3/20,30 
  * Get the Row/Column of the triangle located at [10,20], [10,30], [20,30]
  * Bad results return as { "row": "A", "column": 0 }
