![ClampedLooping](https://user-images.githubusercontent.com/33253710/157117012-306df771-c90f-454c-b049-a409a4739a9e.jpg)
<p align=center>Automatic clamping of common values as well as looping within the range.</p>

# Breakdown

## Value System Base
The base class for this system, handles some of the fields & methods that are used in all inheritors. 

## Clamped Values
Clamped values work as a self contained <code>Math.Clamp</code>. These values can go up or down by their min/max values but no further, instead clamping to the min/max value when reached. 

## Looping Values
Looping values work just like their clamped counterparts but instead of sticking to the mix/max values they loop around to the min/max instead and continue on. 

# Usage
For all the examples below we'll be using a <code>ClampedValueInt</code> but the same code applies to all variants including the looping versions. 

## Declaring 
When declaring a clamped or looping value you will need to use its constructor to create a new verison of it for use. There are some parameters to fill in:
- <code>minValue</code> - Defines the lower bound for the clamped/looping value.
- <code>maxValue</code> - Defines the upper bound for the clamped/looping value.
- <code>startingValue</code> - Defines the starting value for the clamped/looping value.

Below is an example of a clamped int being setup with a range of 0-10 with a starting value of 0, you can also no enter a starting value, in which case it will enter the min value for the starting value:

![ClampedIntDec](https://user-images.githubusercontent.com/33253710/157196944-33f2b826-305b-4dd2-94fd-a7e514df9263.png)

## Incrementing
To edit the value of the variable you use the <code>Increment()</code> method. This method will add whatever value you enter and then clamp or loop dependant on said change. If you want to subtract you just need to use a negative number instead of a positive one. 

An example usage below:

![ClampedIntIncrement](https://user-images.githubusercontent.com/33253710/157199628-07f73bd9-6e58-4880-a7fa-4b0ab04e014c.png)

## Reading the current value
To read the current value all you need to do is get the <code>Value</code> property from the clamped/looping value. An example below:

![ClampedIntValue](https://user-images.githubusercontent.com/33253710/157199058-8fadb29a-b819-44e9-a2b1-9d039543aff4.png)

