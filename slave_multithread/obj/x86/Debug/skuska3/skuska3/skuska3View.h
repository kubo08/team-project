
// skuska3View.h : interface of the Cskuska3View class
//

#pragma once


class Cskuska3View : public CView
{
protected: // create from serialization only
	Cskuska3View();
	DECLARE_DYNCREATE(Cskuska3View)

// Attributes
public:
	Cskuska3Doc* GetDocument() const;

// Operations
public:

// Overrides
public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~Cskuska3View();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	afx_msg void OnFilePrintPreview();
	afx_msg void OnRButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnContextMenu(CWnd* pWnd, CPoint point);
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in skuska3View.cpp
inline Cskuska3Doc* Cskuska3View::GetDocument() const
   { return reinterpret_cast<Cskuska3Doc*>(m_pDocument); }
#endif

