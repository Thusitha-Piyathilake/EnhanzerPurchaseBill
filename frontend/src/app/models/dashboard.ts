export interface LatestPurchaseOrder {
  id: number;
  netAmount: number;
  noOfItems: number;
  createdAt: string;
}

export interface OldestPurchaseOrderItem {
  id: number;
  purchaseOrderId: number;
  item: string;
  batch: string;
  quantity: number;
  totalCost: number;
  purchaseOrderCreatedAt: string;
}

export interface ItemQuantity {
  itemName: string;
  quantity: number;
}