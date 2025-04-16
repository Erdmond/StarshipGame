from polygon import Polygon
import math

def is_overlap(proj1: tuple[int, int], proj2: tuple[int, int]) -> bool:
    """Проверяет перекрытие двух проекций."""
    return proj1[0] <= proj2[1] and proj2[0] <= proj1[1]

class CollisionDetector:
    @staticmethod
    def check_static_collision(poly1: Polygon, poly2: Polygon) -> bool:
        """Проверяет столкновение двух статических объектов c помощью SAT."""
        for poly in [poly1, poly2]:
            for edge in poly.get_edges():
                axis = (-edge[1], edge[0])
                axis_length = math.sqrt(axis[0]**2 + axis[1]**2)
                if axis_length == 0:
                    continue
                axis = (axis[0] / axis_length, axis[1] / axis_length)

                proj1 = poly1.project(axis)
                proj2 = poly2.project(axis)
                if not is_overlap(proj1, proj2):
                    return False
        return True

    @staticmethod
    def check_dynamic_collision(
        moving_poly: Polygon, 
        velocity: tuple[int, int], 
        static_poly: Polygon, 
        delta_time: int = 1
    ) -> bool:
        """Проверяет столкновение движущегося объекта co статическим."""
        steps = 10
        for step in range(steps + 1):
            t = step / steps
            dx = velocity[0] * t * delta_time
            dy = velocity[1] * t * delta_time
            moved_points = [(x + dx, y + dy) for x, y in moving_poly.points]
            if CollisionDetector.check_static_collision(Polygon(moved_points), static_poly):
                return True
        return False
