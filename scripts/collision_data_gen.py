from collision_check import *
import csv

class CollisionDataGenerator:
    def __init__(
        self, 
        static_shape: list[tuple[int, int]], 
        moving_shape: list[tuple[int, int]], 
        max_vel: int = 5
    ):
        self.static_shape = static_shape
        self.moving_shape = moving_shape
        self.max_vel = max_vel

    def generate(self, filename: str):
        static_poly = Polygon(self.static_shape)
        data = []

        for dx in range(-self.max_vel, self.max_vel + 1):
            for dy in range(-self.max_vel, self.max_vel + 1):
                for dvx in range(-self.max_vel, self.max_vel + 1):
                    for dvy in range(-self.max_vel, self.max_vel + 1):
                        moved_points = [(x + dx, y + dy) for x, y in self.moving_shape]
                        moving_poly = Polygon(moved_points)
                        velocity = (dvx, dvy)

                        is_collision = CollisionDetector.check_dynamic_collision(moving_poly, velocity, static_poly)
                        if is_collision: data.append([dx, dy, dvx, dvy, is_collision])

        with open(filename, 'w', newline='') as file:
            writer = csv.writer(file)
            writer.writerows(data)

if __name__ == "__main__":
    static_shape = [(0, 0), (2, 0), (2, 2), (0, 2)]
    moving_shape = [(-0.5, -0.5), (0.5, -0.5), (0.5, 0.5), (-0.5, 0.5)]

    generator = CollisionDataGenerator(static_shape, moving_shape, max_vel=10)
    generator.generate("data/collisions.csv")
