export interface PurchaseOrder {
  id: number;
  netAmount: number;
  createdAt: string;
  items: PurchaseOrderItem[];
}

export interface PurchaseOrderItem {
  id: number;
  item: string;
  batch: string;
  standardCost: number;
  standardPrice: number;
  quantity: number;
  discount: number;
  totalCost: number;
  totalSelling: number;
  purchaseOrderId?: number | null;
}