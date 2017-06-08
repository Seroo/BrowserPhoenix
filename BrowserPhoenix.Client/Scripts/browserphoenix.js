function toFixed_norounding(n, p) {
    result = n.toFixed(p);
    return result <= n ? result : (result - Math.pow(0.1, p)).toFixed(p);
}