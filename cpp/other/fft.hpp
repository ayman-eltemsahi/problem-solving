#pragma once

#include <complex>
#include <vector>

const double TAU = 8 * atan(1);

void makeroots(int n) {
  double long m = TAU / n;
  for (int i = 0; i < n; i++) {
    roots[i] = complex<double>(cos(i * m), sin(i * m));
  }
}

void divide(vector<complex<double> >& A, int r) {
  int n = A.size();
  if (n == 1) return;

  vector<complex<double> > even(n / 2);
  vector<complex<double> > odd(n / 2);

  for (int i = 0; i < n; i += 2) {
    even[i / 2] = A[i];
    odd[i / 2] = A[i + 1];
  }
  divide(even, r);
  divide(odd, r);

  int n2 = n / 2, f2 = r / n;
  ;
  for (int j = 0; j < n2; ++j) {
    complex<double> _ = roots[j * f2] * odd[j];
    A[j] = even[j] + _;
    A[j + n2] = even[j] - _;
  }
}

void fft(vector<long long>& res, vector<long long>& a, vector<long long>& b) {
  int len = max(a.size(), b.size());
  int n = 1 << (1 + (int)ceil(log2(len)));

  vector<complex<double> > A(a.begin(), a.end());
  vector<complex<double> > B(b.begin(), b.end());

  A.resize(n);
  B.resize(n);

  makeroots(n);

  divide(A, n);
  divide(B, n);

  for (int i = 0; i < n; i++) {
    A[i] = conj(A[i] * B[i]);
  }

  divide(A, n);

  res.resize(n);
  for (int i = 0; i < n; i++) {
    res[i] = ((long long)round(A[i].real() / n)) % MOD;
  }
}
