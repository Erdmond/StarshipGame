class Polygon:
    def __init__(self, points: list[tuple[int, int]]):
        self.points = points

    def get_edges(self) -> list[tuple[int, int]]:
        """Возвращает векторы рёбер многоугольника."""
        edges = []
        for i in range(len(self.points)):
            x1, y1 = self.points[i]
            x2, y2 = self.points[(i + 1) % len(self.points)]
            edges.append((x2 - x1, y2 - y1))
        return edges

    def project(self, axis: tuple[int, int]) -> tuple[int, int]:
        """Проектирует многоугольник на ось."""
        dots = [x * axis[0] + y * axis[1] for x, y in self.points]
        return (min(dots), max(dots))
