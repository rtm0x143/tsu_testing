## "Combination Sum" algorithm doesn't check argument values constraints.
**Severity:** Minor.\
**Priority:** Medium.\
**Description:** SUT doesn't reject algorithm execution if given parameters don't fits [constraints](https://leetcode.com/problems/combination-sum/).\
**Reproduction:** Always.\
**Environment:** net7.0, x64.\
**Steps to reproduce:**  
1. Create instance of _SolutionApi.Solution_ class.
2. Invoke _SolutionApi.Solution.CombinationSum_ method providing arguments that does not fit [constraints](https://leetcode.com/problems/combination-sum/).

**Expected result:** SUT thrown corresponding exception.\
**Actual result:** SUT executes algorithm.


