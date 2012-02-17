
// skuska3View.cpp : implementation of the Cskuska3View class
//

#include "stdafx.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "skuska3.h"
#endif

#include "skuska3Doc.h"
#include "skuska3View.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// Cskuska3View

IMPLEMENT_DYNCREATE(Cskuska3View, CView)

BEGIN_MESSAGE_MAP(Cskuska3View, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &Cskuska3View::OnFilePrintPreview)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONUP()
END_MESSAGE_MAP()

// Cskuska3View construction/destruction

Cskuska3View::Cskuska3View()
{
	// TODO: add construction code here

}

Cskuska3View::~Cskuska3View()
{
}

BOOL Cskuska3View::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// Cskuska3View drawing

void Cskuska3View::OnDraw(CDC* /*pDC*/)
{
	Cskuska3Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here
}


// Cskuska3View printing


void Cskuska3View::OnFilePrintPreview()
{
#ifndef SHARED_HANDLERS
	AFXPrintPreview(this);
#endif
}

BOOL Cskuska3View::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void Cskuska3View::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void Cskuska3View::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

void Cskuska3View::OnRButtonUp(UINT /* nFlags */, CPoint point)
{
	ClientToScreen(&point);
	OnContextMenu(this, point);
}

void Cskuska3View::OnContextMenu(CWnd* /* pWnd */, CPoint point)
{
#ifndef SHARED_HANDLERS
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
#endif
}


// Cskuska3View diagnostics

#ifdef _DEBUG
void Cskuska3View::AssertValid() const
{
	CView::AssertValid();
}

void Cskuska3View::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

Cskuska3Doc* Cskuska3View::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(Cskuska3Doc)));
	return (Cskuska3Doc*)m_pDocument;
}
#endif //_DEBUG


// Cskuska3View message handlers
