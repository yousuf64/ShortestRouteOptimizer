import axios from 'axios'
import type { ShortestPathData } from '@/types'

export async function fetchNodes(): Promise<string[]> {
  try {
    const response = await axios.get<string[]>('/api/v1/nodes')
    return response.data
  } catch (error) {
    console.error('Error fetching nodes:', error)
    throw error
  }
}

export async function calculateShortestPath(fromNode: string, toNode: string): Promise<ShortestPathData> {
  try {
    const response = await axios.get<ShortestPathData>(`/api/v1/shortest-path/${fromNode}/${toNode}`)
    return response.data
  } catch (error) {
    console.error('Error calculating shortest path:', error)
    throw error
  }
}