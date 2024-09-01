export interface ShortestPathData {
  distance: number
  nodeNames: string[]
}

export interface CalculatorResultModel {
  fromNode: string,
  toNode: string,
  distance: number,
  path: string[]
}