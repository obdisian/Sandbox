using UnityEngine;
using System.Collections;

public static class Interpolation {

	//----------------------------------------
	// Linear
	public static float Linear (float t, float b, float c) {
		c = c - b;
		return c * t / 1.0f + b;
	}

	//----------------------------------------
	// Quadratic
	public static float QuadraticIn (float t, float b, float c) {
		c = c - b; t /= 1.0f;
		return c * t * t + b;
	}
	public static float QuadraticOut (float t, float b, float c) {
		c = c - b; t /= 1.0f;
		return -c * t * (t - 2) + b;
	}
	public static float QuadraticInOut (float t, float b, float c) {
		c = c - b; t /= 1.0f / 2;
		if (t < 1) return c / 2 * t * t + b;
		t--;
		return -c / 2 * (t * (t - 2) - 1) + b;
	}

	//----------------------------------------
	// Cubic
	public static float CubicIn (float t, float b, float c) {
		c = c - b;
		t /= 1.0f;
		return c * t * t * t + b;
	}
	public static float CubicOut (float t, float b, float c) {
		c = c - b;
		t /= 1.0f;
		t--;
		return c * (t * t * t + 1) + b;
	}
	public static float CubicInOut (float t, float b, float c) {
		c = c - b;
		t /= 1.0f / 2;
		if (t < 1) return c / 2 * t * t * t + b;
		t -= 2;
		return c / 2 * (t * t * t + 2) + b;
	}

	//----------------------------------------
	// Quartic
	public static float QuarticIn (float t, float b, float c) {
		c = c - b;
		t /= 1.0f;
		return c * t * t * t * t + b;
	}
	public static float QuarticOut (float t, float b, float c) {
		c = c - b;
		t /= 1.0f;
		t--;
		return -c * (t * t * t * t - 1) + b;
	}
	public static float QuarticInOut (float t, float b, float c) {
		c = c - b;
		t /= 1.0f / 2;
		if (t < 1) return c / 2 * t * t * t * t + b;
		t -= 2;
		return -c / 2 * (t * t * t * t - 2) + b;
	}

	//----------------------------------------
	// Quintic
	public static float QuinticIn (float t, float b, float c) {
		c = c - b;
		t /= 1.0f;
		return c * t * t * t * t * t + b;
	}
	public static float QuinticOut (float t, float b, float c) {
		c = c - b;
		t /= 1.0f;
		t--;
		return c * (t * t * t * t * t + 1) + b;
	}
	public static float QuinticInOut (float t, float b, float c) {
		c = c - b;
		t /= 1.0f / 2;
		if (t < 1) return c / 2 * t * t * t * t * t + b;
		t -= 2;
		return c / 2 * (t * t * t * t * t + 2) + b;
	}

	//----------------------------------------
	// Sinusoidal
	public static float SinusoidalIn (float t, float b, float c) {
		c = c - b;
		return -c * Mathf.Cos (t / 1.0f * (Mathf.PI / 2)) + c + b;
	}
	public static float SinusoidalOut (float t, float b, float c) {
		c = c - b;
		return c * Mathf.Sin (t / 1.0f * (Mathf.PI / 2)) + b;
	}
	public static float SinusoidalInOut (float t, float b, float c) {
		c = c - b;
		return -c / 2 * (Mathf.Cos (Mathf.PI * t / 1.0f) - 1) + b;
	}

	//----------------------------------------
	// Exponential
	public static float ExponentialIn (float t, float b, float c) {
		c = c - b;
		return c * Mathf.Pow (2, 10 * (t / 1.0f - 1)) + b;
	}
	public static float ExponentialOut (float t, float b, float c) {
		c = c - b;
		return c * (-Mathf.Pow (2, -10 * t / 1.0f) + 1) + b;
	}
	public static float ExponentialInOut (float t, float b, float c) {
		c = c - b;
		t /= 1.0f / 2;
		if (t < 1) return c / 2 * Mathf.Pow (2, 10 * (t - 1)) + b;
		t--;
		return c / 2 * (-Mathf.Pow (2, -10 * t) + 2) + b;
	}

	//----------------------------------------
	// Circular
	public static float CircularIn (float t, float b, float c) {
		c = c - b;
		t /= 1.0f;
		return -c * (Mathf.Sqrt (1 - t * t) - 1) + b;
	}
	public static float CircularOut (float t, float b, float c) {
		t /= 1.0f;
		t--;
		return c * Mathf.Sqrt (1 - t * t) + b;
	}
	public static float CircularInOut (float t, float b, float c) {
		t /= 1.0f / 2;
		if (t < 1) return -c / 2 * (Mathf.Sqrt (1 - t * t) - 1) + b;
		t -= 2;
		return c / 2 * (Mathf.Sqrt (1 - t * t) + 1) + b;
	}
}
