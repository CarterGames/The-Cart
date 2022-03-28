![MinMax](https://user-images.githubusercontent.com/33253710/160084530-351b9f71-9d74-47a6-b0d1-5c05e7cba1fc.jpg)

<p align=center>A system for getting a value between a set min and max value.</p>

The Min/Max system is handy for getting values between a min & max range. Most useful for getting a random value between a range that you can define in 1 field instead of multiple. 

# Usage
There are varients for <code>int</code>, <code>float</code> or <code>double</code>. You can define the min and/or max with the constructor for the class or via the inspector, the system has a custom property drawer for ease of use.

![Min max INspector Look](https://user-images.githubusercontent.com/33253710/160472998-e744a097-9703-423b-ac15-7d3d2fd96642.png)

By default the constructor sets a min/max between 0/1 unless you specify the range yourself. If using a serialised version you don't need to initialize it yourself. 

![Min Max Example 2](https://user-images.githubusercontent.com/33253710/160473103-5792395d-0079-410f-b527-e55826fbceb9.png)

To get a value within the range you can just call the <code>Random()</code> like so.

![Min Max Example 1](https://user-images.githubusercontent.com/33253710/160473040-9bd6a65f-8fed-462b-bcbf-35d9e19aa1d6.png)
