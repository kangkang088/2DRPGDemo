local table1 = {}
function table1.clone(table)
    local table_copy = {}
    for key, value in pairs(table) do
        table_copy[key] = value
    end
    return table_copy
end

Animal = {}
Animal.__index = Animal

function Animal:new(name)
    local self = setmetatable({}, Animal)
    self.name = name
    return self
end

function Animal:makeSound()
    print(self.name .. "!")
end

Cat = {}
Cat.__index = Cat
setmetatable(Cat, Animal)

function Cat:new(name)
    local self = Animal:new(name)
    setmetatable(self, Cat)
    return self
end

function Cat:makeSound()
    Animal.makeSound(self)
    print("Meow!")
end