#include <bits/stdc++.h>
using namespace std;
#define forn(i, n) for (int i = 0; i < (int)(n); ++i)
typedef long long i64;

typedef double ld;

ifstream infile("C:\\stars.sublime");

const int maxlg = 16;
const int M = 1 << maxlg;
const int N = 203;

struct base {
	ld re, im;
	base() {
	}
	base(ld re) :
		re(re), im(0) {
	}
	base(ld re, ld im) :
		re(re), im(im) {
	}

	base operator+(const base& o) const {
		return {re+o.re, im+o.im};
	}
	base operator-(const base& o) const {
		return {re-o.re, im-o.im};
	}
	base operator*(const base& o) const {
		return {
            re*o.re - im*o.im,
            re*o.im + im*o.re
        };
	}
};

vector<base> ang[maxlg + 5];

void init_fft() {
	int n = 1 << maxlg;
	ld e = acosl(-1) * 2 / n;
	ang[maxlg].resize(n);
	forn(i, n) {
		ang[maxlg][i] = {cos(e * i), sin(e * i)};
	}

	for (int k = maxlg - 1; k >= 0; --k) {
		ang[k].resize(1 << k);
		forn(i, 1<<k) {
			ang[k][i] = ang[k+1][i*2];
		}
	}
}

void fft_rec(base *a, int lg, bool rev) {
	if (lg == 0) {
		return;
	}
	int len = 1 << (lg - 1);
	fft_rec(a, lg - 1, rev);
	fft_rec(a + len, lg - 1, rev);

	forn(i, len) {
		base w = ang[lg][i];
		if (rev) {
			w.im *= -1;
		}
		base u = a[i];
		base v = a[i + len] * w;
		a[i] = u + v;
		a[i + len] = u - v;
	}
}

//n must be power of 2
void fft(base *a, int n, bool rev) {
	int lg = 0;
	while ((1 << lg) != n) {
		++lg;
	}
	int j = 0, bit;
	for (int i = 1; i < n; ++i) {
		for (bit = n >> 1; bit & j; bit >>= 1)
			j ^= bit;
		j ^= bit;
		if (i < j)
			swap(a[i], a[j]);
	}
	fft_rec(a, lg, rev);
	if (rev)
		forn(i, n) {
			a[i] = a[i] * (1.0 / n);
		}
}

void mul(base *a, base *b, int M = M) {
	forn(i, M)
		a[i] = a[i] * b[i];
}

void add(base *a, base *b, int M = M) {
	forn(i, M)
		a[i] = a[i] + b[i];
}

void mul(base *a, ld by) {
	for (int i = 0; i < M; ++i)
		a[i] = a[i] * by;
}

void setpw(base *a, int k) {
	for (int i = 0; i < M; ++i)
		a[i] = ang[maxlg][(1ll * k * i) & (M - 1)];
}

int n;
int w[N];
vector<int> g[N];
ld c[N];
base D[N][M];
base ONE[M], temp[M], ans[M];
int a[M];
bool used[N];
int sumw;

void clear() {
	for (int i = 0; i < n; ++i)
		g[i].clear();
	fill(ans, ans + M, 0);
	fill(used, used + N, false);
}

void read() {
	infile >> n;

	for (int i = 0; i < n; ++i)
		infile >> w[i];

	sumw = accumulate(w, w + n, 0);

	for (int i = 0; i <= sumw; ++i)
		infile >> a[i];

	for (int i = 0; i < n - 1; ++i) {
		int x, y;
		infile >> x >> y;
		--x, --y;
		g[x].push_back(y);
		g[y].push_back(x);
	}
}

void dfs(int v) {
	used[v] = true;

	setpw(D[v], w[v]);
	//D[v][w[v]] = 1.0;
	c[v] = 1.0;

	for (int to : g[v])
	if (!used[to]) {
		dfs(to);
		//copy(D[to], D[to] + M, temp);

		//mul(temp, c[to]);
		//add(temp, ONE);
		//mul(temp, 1. / (1. + c[to]));
		mul(D[v], D[to]);

		c[v] *= 1. + c[to];
	}

	add(D[v], ONE);
}

void kill() {
	dfs(0);

	ld sum = 0;
	for (int i = 0; i < n; ++i)
		sum += c[i];

	fill(ans, ans + M, 0);

	for (int i = 0; i < n; ++i) {
		//copy(D[i], D[i] + M, temp);
		//mul(temp, c[i] / sum);
		//add(ans, temp);
		add(ans, D[i]);
	}

	fft(ans, M, 1);

	ans[0].re -= n;
	ld tot = 0;
	for (int i = 0; i <= sumw; ++i)
		tot += ans[i].re;

	ld res = 0;

	for (int i = 0; i <= sumw; ++i)
		res += ans[i].re * a[i];

	res /= tot;

	cout << res << endl;
}

int main() {
	cout.precision(15);
	cout << fixed;
	init_fft();
	setpw(ONE, 0);

	int t;
	infile >> t;

	while (t--) {
		read();
		kill();
		clear();
	}
	return 0;
}
